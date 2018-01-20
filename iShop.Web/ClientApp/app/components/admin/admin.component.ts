import { ChangeDetectorRef, TemplateRef, Component,Output,EventEmitter, Input,OnInit } from '@angular/core';

import { trigger, transition, animate, style, keyframes } from '@angular/animations';
import { SharedService } from '../../service/shared-service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Cart} from "../../model/Cart";


@Component({
    selector: 'admin',
    templateUrl: './admin.component.html',
    styleUrls: ['./admin.component.css']
})
export class AdminComponent implements  OnInit {
    ngOnInit(): void {
       
      
      
    }


}
