import { Pipe, PipeTransform } from '@angular/core';


@Pipe({
    name: 'filterCategories',
    pure: false
})
//
export class FilterCategoryPipe implements PipeTransform {
    transform(items: any[], filter: string): any {
        if (!items || !filter) {
            return items;
        }
       

        return items.filter(c => c.short.indexOf(filter)!==-1);

    }
}  