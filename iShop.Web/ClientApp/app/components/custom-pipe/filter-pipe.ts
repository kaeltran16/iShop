import { Pipe, PipeTransform } from '@angular/core';


@Pipe({
    name: 'filter',
    pure: false
})
//
export class FilterPipe implements PipeTransform {
    transform(items: any[], filter: string): any {
        if (!items || !filter) {
            return items;
        }
       
        filter = filter.toLowerCase();
        return items.filter((p, i: any, ps: any) => {
            let categories = p.categories.filter((c: any) => c.name.toLowerCase().indexOf(filter) !== -1);
           
            if (categories.length) return true;
            return false;
        });
    }
}  