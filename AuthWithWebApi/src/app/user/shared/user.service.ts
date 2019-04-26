import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly baseUrl = 'http://localhost:54898';

  constructor(private http: HttpClient) { }

  registerUser(user: User, selectedRoles: string[]) {
    const dataToRegister = {
      userName: user.userName,
      password: user.password,
      email: user.email,
      firstName: user.firstName,
      lastName: user.lastName,
      roles: selectedRoles
    };
    const requestHeader = new HttpHeaders({ 'No-Auth': 'True' });
    return this.http.post(this.baseUrl + '/api/user/register',
      dataToRegister, { headers: requestHeader });
  }

  userAuthentication(userName: string, password: string) {
    const requestBody = 'username=' + userName +
                        '&password=' + password + '&grant_type=password';
    const requestHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded',
                          'No-Auth': 'True' });
    return this.http.post(this.baseUrl + '/token', requestBody, { headers: requestHeader });
  }

  getUserDetailsFromClaims() {
    return this.http.get(this.baseUrl + '/api/GetUserClaims');
  }

  getAllRoles() {
    const requestHeader = new HttpHeaders({ 'No-Auth': 'True' });
    return this.http.get(this.baseUrl + '/api/GetAllRoles', { headers: requestHeader });
  }

  matchRole(allowedRoles): boolean {
    let isMatch = false;
    const userRoles: string[] = JSON.parse(localStorage.getItem('userRoles'));
    allowedRoles.forEach(role => {
      if (userRoles.indexOf(role) > -1) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
}
