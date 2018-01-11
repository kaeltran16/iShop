import { Component, Input } from '@angular/core';

@Component({
    selector: 'order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.css']
})
export class OrderComponent {
    outline: boolean = true;
    chooseAddress(outline:boolean) {
        this.outline = outline;
    }
    
}
