import { Component, Output, EventEmitter, Input ,OnInit} from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';
import { UserService } from '../../service/user.service';
import { Login } from "../../model/User";
import { Observable } from 'rxjs/Rx';
import { SharedService } from '../../service/shared-service';
@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    animations: [
      
      
    ]
})
export class LoginComponent implements OnInit {
    ngOnInit(): void {
      
    }

    username: string="";
    password: string = "";
    result:boolean=false;
    @Output('onclick') onclick = new EventEmitter<any>();
   
    countTime:number=1800000;
        
   
    constructor(private userService: UserService, private sharedService: SharedService) {
        
    }
    //login
    login($event: any) {
        if ($event.valid) {
            var user = new Login(this.username, this.password);
            //get token
            var token = this.userService.login(user);
            setTimeout(() => {
                localStorage.removeItem("token");
                this.sharedService.emitChangeToken({"lastName":"Đăng Nhập"});
            }, this.countTime);
            token.subscribe(t => {
               
                localStorage.setItem("token",t.access_token );
                this.userService.info(t.access_token).subscribe(t => {
                        //output
                        this.onclick.emit(true);
                        this.sharedService.emitChangeToken(t);
                });
                 
                    
                }, error => this.result = true // show label error when login fail 
            );

        }
    }

   
   

}
