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
        return items.filter((p, i: any, ps: any) => p.title === filter);
    }
}  