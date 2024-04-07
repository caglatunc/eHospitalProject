export class PatientAppointmentModel{
    id: string = "";
    doctorName: string = "";
    doctorSpecialty: string = "";
    startDate: Date = new Date();
    endDate: Date = new Date();
    isItFinished: boolean = false;
}