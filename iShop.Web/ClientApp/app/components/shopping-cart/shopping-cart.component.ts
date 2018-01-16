import { Component, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
@Component({
    selector: 'shopping-cart',
    templateUrl: './shopping-cart.component.html',
    styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent {
    constructor( private route: ActivatedRoute,
                 private router: Router) {
        
    }

    checkOut() {
        this.router.navigate(['/order']);
    }
    
}
