import { Component, ElementRef, OnInit, ViewChild, viewChild} from '@angular/core';
import { DxSchedulerModule } from 'devextreme-angular';
import { UserModel } from '../../../models/user.model';
import { HttpClient } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormValidateDirective } from 'form-validate-angular';
import { Appointment } from '../../app.service';
import { AppointmentModel } from '../../../models/appointment.modeL';
import { ResultModel } from '../../../models/result.model';

declare const $: any;

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

  addModel: AppointmentModel = new AppointmentModel();

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

  onAppointmentFormOpening(event: any) {
    event.cancel = true;
   $("#addAppointmentModal").modal('show');
  }

  add(form:NgForm){
    if(form.valid){
      $("#addAppointmentModal").modal('hide');
    }
  }

  findPatientByIdentityNumber(){
if(this.addModel.patient.identityNumber.length < 11) return;
    this.http.post<ResultModel<UserModel>>
    (`https://localhost:7204/api/Appointments/FindPatientByIdentityNumber`, {identityNumber: this.addModel.patient.identityNumber}).subscribe((res)=>{
        if(res.data !== undefined && res.data !== null){
          this.addModel.patient = res.data;
        }
    });
  }
}