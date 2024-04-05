import { Specialty, UserModel } from "./user.model";

export class AppointmentModel {
    id: string = "";
    doctorId: string = "";
    doctor: UserModel = new UserModel();
    patientId: string = "";
    patient: UserModel = new UserModel();
    startDate: string = "";
    endDate: string = "";
    epicrisisReport: string = "";
    price:number = 0;
    isItFinished: boolean = false;
}

export class getPatientAppointmentsDetailModel {
    appointmentId: string = "";
    doctorName: string = "";
    specialty: Specialty = Specialty.GeneralMedicine; // Enum kullanımı
    startDate: Date | string = new Date();
    endDate: Date | string = new Date();
}