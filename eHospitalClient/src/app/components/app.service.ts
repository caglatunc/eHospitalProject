import { Injectable } from '@angular/core';

export class Priority {
  text: string = ''; // varsayılan bir string ile başlatıldı
  id: number = 0; // varsayılan bir sayı ile başlatıldı
  color: string = '#FFFFFF'; // varsayılan bir renk değeri ile başlatıldı
}

export class Resource {
  text: string = '';
  id: number = 0;
  color: string = '#FFFFFF';
}

export class Appointment {
  text: string = '';
  ownerId: number[] = []; // varsayılan olarak boş bir dizi ile başlatıldı
  priority: number = 0;
  startDate: Date = new Date(); // varsayılan olarak şimdiki zaman ile başlatıldı
  endDate: Date = new Date();
}

@Injectable({
  providedIn: 'root'
})
export class AppService {
  private prioritiesData: Priority[] = [
    { text: 'Low Priority', id: 1, color: '#1e90ff' },
    { text: 'High Priority', id: 2, color: '#ff9747' }
  ];

  private resourcesData: Resource[] = [
    { text: 'Samantha Bright', id: 1, color: '#cb6bb2' },
    { text: 'John Heart', id: 2, color: '#56ca85' }
  ];

  private appointments: Appointment[] = [
    {
      text: 'Strategy Meeting',
      ownerId: [1], // Bu Resource ID'siyle eşleşmeli
      priority: 1, // Bu Priority ID'siyle eşleşmeli
      startDate: new Date(2024, 2, 24, 9, 30), // Yıl, ay (0 indexli), gün, saat, dakika
      endDate: new Date(2024, 2, 24, 11, 0)
    },
  ];
  getAppointments() {
    return this.appointments;
  }

  getPriorities() {
    return this.prioritiesData;
  }

  getResources() {
    return this.resourcesData;
  }
  constructor() { }
  
}
