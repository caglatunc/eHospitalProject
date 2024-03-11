import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { DxSchedulerModule } from 'devextreme-angular';
import { UserModel } from '../../../models/user.model';
import { HttpClient } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';

declare var $: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    DxSchedulerModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  appointmentsData: any[] = [];
  selectedDoctorId: string = "";
  currentDate: Date = new Date();
  doctors: UserModel[] = [];
  patients: UserModel[] = [];

  addPatientModel: UserModel = new UserModel();

  @ViewChild('patientAddModalCloseBtn', { static: false }) patientAddModalCloseBtn!: ElementRef<HTMLButtonElement>;

  isPatientModalVisible: boolean = false; // Modal'ın görünürlüğünü kontrol edecek
  currentAppointmentData: any = null; // Seçilen randevu bilgilerini saklayacak


  constructor(
    private http: HttpClient
  ) { }


  ngOnInit(): void {
    this.getAllDoctors();
  }



  getAllDoctors() {
    this.http.get("https://localhost:7204/api/Doctors/GetAllDoctors").subscribe((res: any) => {
      this.doctors = res.data
    })
  }

  getDoctorAppointments() {
    if (this.selectedDoctorId === "") return;

    this.http.get(`https://localhost:7204/api/Appointments/GetAllAppointmentByDoktorId?doctorId=${this.selectedDoctorId}`).subscribe
      ((res: any) => {

        console.log(res.data);

        const data = res.data.map((val: any, i: number) => {
          return {
            text: val.patient.fullName,
            startDate: new Date(val.startDate),
            endDate: new Date(val.endDate)
          }
        })
        this.appointmentsData = data;
      })
  }

  onAppointmentFormOpening(e: any) {
    e.cancel = true; // Bu varsayılan formun açılmasını önler
    console.log('Cell double clicked', e); // Bu satırı ekleyin
    // Kendi modal açma fonksiyonunuzu burada çağırın
    this.openPatientAppointmentModal(e.appointmentData);
  }

  openPatientAppointmentModal(appointmentData: any) {
    this.currentAppointmentData = appointmentData; // Randevu verisini saklayın
    this.isPatientModalVisible = true; // Modal'ı göster
    console.log('Modal should open now');
  }
  closePatientModal() {
    this.isPatientModalVisible = false; // Modal'ı gizle
  }

  createPatient(form: NgForm) {
    if (form.valid) {
      // Hasta kayıtlı mı diye kontrol et (E-posta veya kimlik numarası ile sorgulama yapılabilir)
      this.isPatientExists(this.addPatientModel.email).subscribe(exists => {
      if (exists) {
      //  Hasta kayıtlıysa, mevcut hasta bilgisi ile randevu oluştur.
       } else {
      //     // Hasta kayıtlı değilse, yeni hasta oluştur ve randevu kaydet.
        }
     });
      
      // Eğer hasta zaten seçildiyse ve id bilgisi 0 değilse, randevuyu oluştur.
      if (this.addPatientModel.id && this.addPatientModel.id !== "0") {
        // Randevu oluşturma işlemi
        this.createAppointment(this.addPatientModel);
      } else {
        // Yeni hasta oluşturma işlemi
        this.http.post("https://localhost:7204/api/Appointments/CreatePatient", this.addPatientModel).subscribe({
          next: (res) => {
            console.log('Creating patient:', res);
            // Yeni hasta oluşturulduktan sonra randevu oluşturma işlemi
            this.createAppointment(res.id);
          },
          error: (err) => {
            console.error('Error creating patient:', err);
          }
        });
      }
    } else {
      alert('Form is not valid');
    }
  }
  
 createAppointment(patientId: string){
    const appointmentData = {
      doctorId: this.selectedDoctorId,
      patientId: patientId,
      startDate: this.currentAppointmentData.startDate,
      endDate: this.currentAppointmentData.endDate
    };
    this.http.post("https://localhost:7204/api/Appointments/CreateAppointment", appointmentData).subscribe({
      next: (res) => {
        console.log('Creating appointment:', res);
        this.getDoctorAppointments();
        this.closePatientModal();
      },
      error: (err) => {
        console.error('Error creating appointment:', err);
      }
    });
 }

}
  
