

export class Order {

    userId: string;
    shipping:any;
    carts: any[] = [];
    constructor(userId: string, carts: any[], shipping: any = {}) {

        this.userId = userId;
        this.carts = carts;
        this.shipping = shipping;
    }
 
}