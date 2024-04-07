import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-layouts',
  standalone: true,
  imports: [RouterOutlet,RouterLink],
  templateUrl:"./layouts.component.html"
})
export class LayoutsComponent {
constructor(
  private router: Router,
  public auth: AuthService
){}

ngOnInit(){


}
logout(){
  localStorage.clear();
  this.router.navigateByUrl("/login");
}
}
