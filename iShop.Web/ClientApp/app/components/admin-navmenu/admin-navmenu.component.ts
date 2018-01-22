import { ChangeDetectorRef, TemplateRef, Component,Output,EventEmitter, Input,OnInit } from '@angular/core';

import { trigger, transition, animate, style, keyframes } from '@angular/animations';
import { SharedService } from '../../service/shared-service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Cart} from "../../model/Cart";
import { Observable } from 'rxjs';
import * as $ from 'jquery';
@Component({
    selector: 'admin-navmenu',
    templateUrl: './admin-navmenu.component.html',
    styleUrls: ['./admin-navmenu.component.css']
})
export class AdminMenuComponent implements  OnInit {
    ngOnInit(): void {
       
//        const arr = [1, 2, 3, 4, 5];
//        const arrObservable = Observable.from(arr).subscribe(val => console.log(val));
//        const arrObservable1 = Observable.from(arr).delay(3000)
//            .subscribe(val => console.log(val));


        
    }
    }



