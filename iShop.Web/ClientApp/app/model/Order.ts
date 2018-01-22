

export class Order {

    userId: string;
 
    orderedItems: any[] = [];
    constructor(userId: string, orderedItems: any[]) {

        this.userId = userId;
        this.orderedItems = orderedItems;
       
    }
 
}