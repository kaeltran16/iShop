import { Component } from '@angular/core';
import { SharedService } from '../../service/shared-service';
@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    infoCart:any;
    constructor(
        private _sharedService: SharedService
    ) {
        _sharedService.changeEmitted$.subscribe(
            info => {
                this.infoCart = info;
            });
    }
}
