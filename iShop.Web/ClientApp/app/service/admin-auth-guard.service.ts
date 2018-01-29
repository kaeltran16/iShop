import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { UserService } from './user.service';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class AdminAuthGuardService implements CanActivate {

    constructor(private userService: UserService, private router: Router,) { }
    canActivate(): Observable<boolean> {
        let token = localStorage.getItem("token");
     

        return this.userService.info(token).map((info: any) => {
           if (info.roles[0] !== "SuperUser") this.router.navigate(['/**']);
            return info.roles[0] === "SuperUser";
        });
    }
}
