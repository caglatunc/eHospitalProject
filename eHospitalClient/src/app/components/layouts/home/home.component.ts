import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { DxSchedulerModule } from 'devextreme-angular';
import { UserModel } from '../../../models/user.model';
import { HttpClient } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SwalService } from '../../services/swal.service';
import { FormValidateDirective } from 'form-validate-angular';
import { AppointmentModel } from '../../../models/appointment.modeL';



declare var $: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    DxSchedulerModule,
    FormsModule,
    CommonModule,
    FormValidateDirective
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  appointmentsData: any[] = [];
  selectedDoctorId: string = "";
  currentDate: Date = new Date();
  doctors: UserModel[] = [];
  
  addPatientModel: UserModel = new UserModel();
  addAppointmentModel: AppointmentModel = new AppointmentModel();

  @ViewChild('patientAddModalCloseBtn', { static: false }) patientAddModalCloseBtn!: ElementRef<HTMLButtonElement>;

  isAppointmentModalVisible: boolean = false; // Randevu ekleme Modal'ın görünürlüğünü kontrol edecek
  isPatientModalVisible: boolean = false; // Hasta ekleme modalının görünürlüğünü kontrol edecek
  appointmentData: any = null; // Seçilen randevu bilgilerini saklayacak


  constructor(
    private http: HttpClient,
    private swal: SwalService
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

//Scheduler Üzerinde Çift Tıklama Olayını Yönetme
  onAppointmentFormOpening(e: any) {
    e.cancel = true; // Varsayılan form açılışını engelle
    if (e.appointmentData) {
      // appointmentData içinde startDate ve endDate'in olması beklenir
      this.appointmentData = {
        startDate: e.appointmentData.startDate,
        endDate: e.appointmentData.endDate,
        // Diğer gerekli alanlar...
      };
      
      // Randevu oluşturma modali göster
      this.isAppointmentModalVisible = true;
    } else {
      // appointmentData içinde gerekli bilgiler yoksa hata göster
      this.swal.callToast('Appointment data is incomplete.', 'error');
    }
  }

  // Randevu oluşturma modalını açan metod
  openAppointmentModal(): void {
    this.resetAppointmentForm(); // Formu sıfırla veya varsayılan değerler atayabilirsiniz
    this.isAppointmentModalVisible = true; // Randevu oluşturma modalını göster
  }
 // Randevu oluşturma formunu gönderen metod
  submitAppointmentForm(form: NgForm) {
    if (form.valid) {
      
      // API'ye randevu oluşturma isteği gönder
      this.http.post('https://localhost:7204/api/Appointments/CreateAppointment', this.appointmentData).subscribe({
        next: (res) => {
          this.isAppointmentModalVisible = false; // Modalı kapat
          this.swal.callToast('Appointment created successfully!', 'success');
          // Randevuları tekrar yükleyin veya güncelleyin
        },
        error: (err) => {
          this.swal.callToast('Error creating appointment.', 'error');
        }
      });
    } else {
      this.swal.callToast('Please fill in all required fields.', 'error');
    }
  }

   // Randevu oluşturma modalını kapatan metod
   closeAppointmentModal() {
    this.isAppointmentModalVisible = false;
  }

  // Hasta ekleme modalını açan metod
  openAddPatientModal(): void {
    this.resetPatientForm();// Formu temizler (yeni girişler için)
    this.isPatientModalVisible = true; // Hasta ekleme modalını göster
  }

  // Hasta ekleme formunu gönderen metod
  submitAddPatientForm(form: NgForm) {
    if (!form.valid) {
      this.swal.callToast('Please fill in all required fields.', 'warning');
      return;
    }
      const identityNumber = this.addPatientModel.identityNumber;
      this.http.get(`https://localhost:7204/api/Users/FindPatientWithIdentityNumber?identityNumber=${identityNumber}`)
        .subscribe({
          next: (response: any) => {
            if(response && response.data) {
              // Hasta sisteme kayıtlı, randevu oluştur
              this.openAppointmentModal();
              this.createAppointment(response.data.id);
              this.swal.callToast('Patient found, creating appointment!', 'success');
            } else {
              // Hasta sisteme kayıtlı değil, hasta ekleme modalını aç
              this.openAddPatientModal();
              this.swal.callToast('Patient not found, please add new patient!', 'warning');
            }
          },
          error: (error) => {
            // Hata oluştu
            console.error('Error fetching patient data:', error);
            this.swal.callToast('An error occurred while searching for the patient.', 'error');
          }
        });
     
      this.swal.callToast('Please enter a valid identity number.', 'warning');
  }
  
// Hasta ekleme modalını kapatan metod
  closeAddPatientModal() {
    this.isPatientModalVisible = false;
  }



  // Randevu oluşturmak için ortak kullanılan metod
  createAppointment(patientId: string) {
    const appointmentData = {
      doctorId: this.selectedDoctorId,
      patientId: patientId,
      startDate: this.appointmentData.startDate,
      endDate: this.appointmentData.endDate
    };
    this.http.post('https://localhost:7204/api/Appointments/CreateAppointment', appointmentData).subscribe({
      next: (res) => {
        this.isAppointmentModalVisible = false; // Modalı kapat
        this.swal.callToast('Appointment created successfully!', 'success');
        // Randevuları tekrar yükleyin veya güncelleyin
      },
      error: (err) => {
        this.swal.callToast('Error creating appointment.', 'error');
      }
    });
  }
 
  resetPatientForm() {
    // ... form sıfırlama işlemleri ...
  }

  resetAppointmentForm() {
    // ... form sıfırlama işlemleri ...
  }
}

