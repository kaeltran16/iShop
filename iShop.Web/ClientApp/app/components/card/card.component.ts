import { ChangeDetectorRef, TemplateRef, Component,Output,EventEmitter, Input,OnInit } from '@angular/core';

import { trigger, transition, animate, style, keyframes } from '@angular/animations';
import { SharedService } from '../../service/shared-service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Cart} from "../../model/Cart";


@Component({
    selector: 'card',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.css'],
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
export class CardComponent implements  OnInit {
    ngOnInit(): void {
        setTimeout(() => {
                this.sharedService.emitChange(false);
            },
            1000);
      
      
    }

    @Input('product') product: any;
    quantity: number = 1;
   
    add: boolean = false;
    isHover:boolean=false;
    modalRef: BsModalRef;
    constructor(private modalService: BsModalService, private sharedService: SharedService) {
        
    }
    addToCart() {
        this.add = true;
        var currentCart = JSON.parse(String(localStorage.getItem(this.product.id)));
       
        if (currentCart) {
            this.quantity = currentCart.quantity;
            this.quantity++;
        }

        //set local storage
        let cart: Cart = new Cart(this.product.id, this.quantity);
        localStorage.setItem(this.product.id, JSON.stringify(cart));
        // close alert 
        this.isHover = false;
     
        
       
        // call shared service 
        this.sharedService.emitChange(true);
        var a = localStorage.getItem(this.product.id);
        var b = JSON.parse(String(a));

        
    }


  
   
    openModal(template: TemplateRef<any>) {
     if(!this.add)
            this.modalRef = this.modalService.show(template);
            
        
    }

    hovering() {
        this.isHover = true;
    }
    leaving() {
        this.isHover = false;
        this.add = false;
    }
    exitDetail(isExit: boolean) {
        this.modalRef.hide();
    }

}
