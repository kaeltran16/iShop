import { Component, Output, EventEmitter,Input } from '@angular/core';

@Component({
    selector: 'detail-product',
    templateUrl: './detail-product.component.html',
    styleUrls: ['./detail-product.component.css']
})
export class  DetailProductComponent {
    @Output() onclick = new EventEmitter<boolean>();
    @Input('product') product:any;

    quantity:number=1;
    constructor() {
        
    }

    exit() {
        this.onclick.emit(false);
    }

    changeValue(isChange: boolean) {

        isChange ? this.quantity++ : this.quantity--;
    }


}
