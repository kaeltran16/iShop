import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../service/product.service';
import { Product } from "../../model/product";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})



export class HomeComponent implements OnInit {
    products: Product[];
    filterargs = { Title: 'Tôm ni trắng' };
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
    constructor(private productService: ProductService) {

    }
    exitDetail(isExit: boolean) {
        this.exit = isExit;
    }
    onDetail(p: Product) {
        this.product = p;
        this.exitDetail(true);
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
