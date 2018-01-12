import { Component, Input } from '@angular/core';

@Component({
    selector: 'card',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.css']
})
export class CardComponent {
    @Input('product') product: any;
    click() {
        alert("jkajsjlsa");
    }

}
