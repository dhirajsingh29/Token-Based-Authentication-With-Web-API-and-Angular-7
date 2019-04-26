import { Component, OnInit } from '@angular/core';
import { UserService } from '../user/shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userClaims: any;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.getUserClaims();
  }

  getUserClaims() {
    this.userService.getUserDetailsFromClaims()
      .subscribe((response: any) => {
        this.userClaims = response;
      });
  }

  logout() {
    localStorage.removeItem('userToken');
    this.router.navigate(['/login']);
  }
}
