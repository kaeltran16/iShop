import { Component, Output, EventEmitter,Input } from '@angular/core';
//import { CookieService } from 'ngx-cookie-service';

@Component({
    selector: 'detail-product',
    templateUrl: './detail-product.component.html',
    styleUrls: ['./detail-product.component.css']
})
export class  DetailProductComponent {
    @Output() onclick = new EventEmitter<boolean>();
    @Input('product') product:any;
    max: number = 5;
    rate: number = 4;
    isReadonly: boolean = true;
    quantity: number = 1;
//    private cookieService: CookieService
    constructor() {
      
        
    }

    addToCart() {
        var alphas :any;
        alphas = [{"hoi":"hoidaica"}, "2", "3", "4"];
        this.onclick.emit(true);
        localStorage.setItem("someKey", alphas);
//        this.cookieService.set('Test', alphas);
        var a = localStorage.getItem("someKey");
        
//        console.log(this.cookieService.get('Test'));

    }

    changeValue(isChange: boolean) {

        isChange ? this.quantity++ : this.quantity--;
    }


}
