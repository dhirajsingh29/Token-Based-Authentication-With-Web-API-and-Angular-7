import { Component, OnInit } from '@angular/core';
import { User } from '../shared/user.model';
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  user: User;
  emailPattern = '^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$';
  roles: any[];

  constructor(private userService: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.resetForm();

    this.userService.getAllRoles()
      .subscribe((response: any) => {
        response.forEach(role => role.selected = false);
        this.roles = response;
      });
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.reset();
    }
    this.user = {
      userName: '',
      password: '',
      email: '',
      firstName: '',
      lastName: ''
    };
    if (this.roles) {
      this.roles.map(role => role.selected = false);
    }
  }

  toggleSelection(index: number) {
    this.roles[index].selected = !this.roles[index].selected;
  }

  registerUser(form: NgForm) {
    const selectedRoles = this.roles.filter(role => role.selected).map(x => x.Name);

    this.userService.registerUser(form.value, selectedRoles)
      .subscribe((data: any) => {
        if (data.Succeeded) {
          this.resetForm(form);
          this.toastr.success('Registered successfully!!');
        } else {
          this.toastr.error(data.Errors[0]);
        }
      });
  }

}
