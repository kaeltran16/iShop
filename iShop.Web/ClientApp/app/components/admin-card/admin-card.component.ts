import { ChangeDetectorRef, TemplateRef, Component,Output,EventEmitter, Input,OnInit } from '@angular/core';

import { trigger, transition, animate, style, keyframes } from '@angular/animations';
import { SharedService } from '../../service/shared-service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Cart} from "../../model/Cart";


@Component({
    selector: 'admin-card',
    templateUrl: './admin-card.component.html',
    styleUrls: ['./admin-card.component.css'],
    animations: [
        trigger('translate', [

            transition(':enter', animate('2000ms ease-in', keyframes([
                style({ opacity: 0, transform: 'translateY(-100px)', offset: 0 }),
                style({ opacity: 1, transform: 'translateY(0px)', offset: 0.5 }),

            ])))
            ,

            transition(':leave', animate('2000ms ease-out', keyframes([
                style({ opacity: 1, transform: 'translateY(0px)', offset: 0 }),
                style({ opacity: 0, transform: 'translateY(-60px)', offset: 0.5 }),

            ])))

        ]),
        trigger('translateBottom', [

            transition(':enter', animate('2000ms ease-in', keyframes([
                style({ opacity: 0, transform: 'translateY(50px)', offset: 0 }),
                style({ opacity: 1, transform: 'translateY(0px)', offset: 0.5 }),

            ])))
            ,

            transition(':leave', animate('2000ms ease-out', keyframes([
                style({ opacity: 1, transform: 'translateY(0px)', offset: 0 }),
                style({ opacity: 0, transform: 'translateY(50px)', offset: 0.5 })

            ])))

        ])]
    

})
export class AdminCardComponent implements  OnInit {
    ngOnInit(): void {
        setTimeout(() => {
                this.sharedService.emitChange(false);
            },
            1000);
     
      
    }

    @Input('product') product: any;

   //delete card
    isDelete:boolean=false;
    isHover:boolean=false;
    modalRef: BsModalRef;
    constructor(private modalService: BsModalService, private sharedService: SharedService) {
        
    }
   


  
   
    openModal(template: TemplateRef<any>) {
            this.modalRef = this.modalService.show(template);
    }

    // when hover in product
    hovering() {
        this.isHover = true;
    }
    leaving() {
        this.isHover = false;
       
    }

    exitDetail(p: any) {
        if (p) {
            if (typeof p === 'boolean') {
                this.modalRef.hide();
                this.isDelete=true;
                return;
            }
            this.modalRef.hide();
            this.product = p;
        }
    }

}
