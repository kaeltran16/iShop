import { ChangeDetectorRef, TemplateRef, Component,Output,EventEmitter, Input } from '@angular/core';
import { Product } from "../../model/product";

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
@Component({
    selector: 'card',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.css']
})
export class CardComponent {
    @Input('product') product: any;
 add:boolean=false;
    modalRef: BsModalRef;
    addToCard() {
        this.add = true;
       alert("con moe may");

    }
   
    constructor( private modalService: BsModalService) {
      
    }
    openModal(template: TemplateRef<any>) {
     if(!this.add)
            this.modalRef = this.modalService.show(template);
            
        
    }

    exitDetail(isExit: boolean) {
        this.modalRef.hide();
    }

}
