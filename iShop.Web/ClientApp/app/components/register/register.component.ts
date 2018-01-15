import { Component, Output, EventEmitter} from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';
import { User } from "../../model/User";
import { UserService } from '../../service/user.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import 'rxjs/add/operator/catch';
@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css'],
    animations: [
       
      
    ]
})
export class RegisterComponent {
    firstName: string;
    lastName: string;
    password: string;
    confirmPassword: string;
    telephone: string;
    ward: string;
    district:string;
    email:string;
    @Output('onclick') onclick = new EventEmitter<boolean>();// when click login then  hidden dialog 

    constructor(private userService: UserService, private route: ActivatedRoute,
        private router: Router) {
      
    }

    async register($event:any) {
        var user = new User(this.firstName, this.lastName, this.password, this.email, this.ward, this.district);
        //create user
        this.userService.createUser(user);

        if ($event.valid) {
            {
                this.router.navigate(['/home']);
                this.onclick.emit(true);
                
            }
          
        }

    }


    
}
