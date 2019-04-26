import { Injectable } from '@angular/core';
import {
    CanActivate, Router,
    ActivatedRouteSnapshot, RouterStateSnapshot
} from '@angular/router';
import { UserService } from '../user/shared/user.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router, private userService: UserService) { }

    canActivate(next: ActivatedRouteSnapshot,
                state: RouterStateSnapshot): boolean {
        if (localStorage.getItem('userToken') != null) {
            // in next.data data is the property defined for adminPanel route
            const roles = next.data.roles as Array<string>;
            if (roles) {
                const match = this.userService.matchRole(roles);
                if (match) {
                    return true;
                } else {
                    this.router.navigate(['/forbidden']);
                    return false;
                }
            } else {
                return true;
            }
        }
        this.router.navigate(['/login']);
        return false;
    }
}
