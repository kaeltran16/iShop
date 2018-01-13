import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { RatingModule } from 'ngx-bootstrap/rating';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PopoverModule } from 'ngx-bootstrap/popover';


//service 
import { ProductService } from "./service/product.service";
import { UserService } from "./service/user.service";




import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FilterPipe } from './components/custom-pipe/filter-pipe';
import { FillPipe } from './components/custom-pipe/fill-pipe';
import { DetailProductComponent } from './components/detail-product/detail-product.component';
import { MoreProductComponent } from './components/more-product/more-product.component';
import { CardComponent } from './components/card/card.component';
import { ItemMenuComponent } from './components/item-menu/item-menu.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FooterComponent } from './components/footer/footer.component';
import { CartComponent } from './components/cart/cart.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { SlideImageComponent } from './components/slide-image/slide-image.component';
import { OrderComponent } from './components/order/order.component';
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
        FillPipe,
        FooterComponent,
        CartComponent,
        ShoppingCartComponent,
        SlideImageComponent,
        OrderComponent
    ],
    imports: [
        RatingModule.forRoot(),
        AccordionModule.forRoot(),
        CarouselModule.forRoot(),
        ModalModule.forRoot(),
        PopoverModule.forRoot(),

        BrowserAnimationsModule,
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'more-product', component: MoreProductComponent },
            { path: 'shopping-cart', component: ShoppingCartComponent },
            { path: 'order', component: OrderComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        ProductService,
        UserService
    ]
})
export class AppModuleShared {
}
