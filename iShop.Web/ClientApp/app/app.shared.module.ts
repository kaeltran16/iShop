import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ProductService } from "./service/product.service";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FilterPipe } from './components/custom-pipe/filter-pipe';
import { FillPipe } from './components/custom-pipe/fill-pipe';
import { DetailProductComponent } from './components/detail-product/detail-product.component';
import { MoreProductComponent } from './components/more-product/more-product.component';
import { CardComponent } from './components/card/card.component';
import { ItemMenuComponent } from './components/item-menu/item-menu.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        DetailProductComponent,
        MoreProductComponent,
        CardComponent,
        ItemMenuComponent,
        LoginComponent,
        RegisterComponent,
        HomeComponent,
        FilterPipe,
        FillPipe
    ],
    imports: [
        AccordionModule.forRoot(),
        CarouselModule.forRoot(),
        BrowserAnimationsModule,
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
      
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        ProductService,
     
    ]
})
export class AppModuleShared {
}
