import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  isLoginError = false;
  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
  }

  login(username: string, password: string) {
    this.userService.userAuthentication(username, password)
      .subscribe((response: any) => {
        // store the access_token in localStorage
        localStorage.setItem('userToken', response.access_token);
        // store user roles in localStorage
        localStorage.setItem('userRoles', response.role);
        this.router.navigate(['/home']);
      }, (error: HttpErrorResponse) => {
        this.isLoginError = true;
      });
  }

}
