
import { ProductService } from '../../service/product.service';
import { Product } from "../../model/product";
import { ChangeDetectorRef, TemplateRef, Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import 'rxjs/add/observable/combineLatest';
@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})



export class HomeComponent implements OnInit {
   
   
    startPopular: number = 1;
    endPopular: number = 3;
    viewProduct: boolean = false;
    startHot: number = 1;
    endHot: number = 3;
    startMeet: number = 1;
    endMeet: number = 3;
    startSeaFood: number = 1;
    endSeaFood: number = 3;
    exit: boolean = false;
    product: Product;
    modalRef: BsModalRef;
    
 
    constructor(private productService: ProductService, private modalService: BsModalService) {
        this.productService.getProducts().subscribe(p => console.log(p));
    }
    exitDetail(isExit: boolean) {
        this.exit = isExit;
    }
    onDetail(p: Product) {
        this.product = p;
      
    }
    openModal(template: TemplateRef<any>,p:Product) {
        this.modalRef = this.modalService.show(template);
        this.product = p;
    }

    ngOnInit() {
      

    }

    nextPopular() {
        if (this.endPopular < 10) {
            this.startPopular += 2;
            this.endPopular += 2;
            if (this.endPopular === 9) {
                this.viewProduct = true;
            }
        }



    }
    nextHot() {
        if (this.endPopular < 10) {
            this.startHot += 2;
            this.endHot += 2;
        }
    }
    nextMeet() {
        if (this.endPopular < 10) {
            this.startMeet += 2;
            this.endMeet += 2;
        }
    }
    nextSeaFood() {
        if (this.endPopular < 10) {
            this.startSeaFood += 2;
            this.endSeaFood += 2;
        }
    }


    prePopular() {
        if (this.startPopular > 1) {
            this.startPopular -= 2;
            this.endPopular -= 2;
        }
    }
    preHot() {
        if (this.startHot > 1) {
            this.startHot -= 2;
            this.endHot -= 2;
        }
    }
    preMeet() {
        if (this.startMeet > 1) {
            this.startMeet -= 2;
            this.endMeet -= 2;
        }
    }
    preSeaFood() {
        if (this.startSeaFood > 1) {
            this.startSeaFood -= 2;
            this.endSeaFood -= 2;
        }
    }

}
