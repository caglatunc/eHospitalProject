import { Routes } from '@angular/router';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/layouts/home/home.component';

export const routes: Routes = [
    {
        path:"",
        component:LayoutsComponent,
        children:[
            {
                path:"",
                component:HomeComponent
            }
        ]
    }
];
