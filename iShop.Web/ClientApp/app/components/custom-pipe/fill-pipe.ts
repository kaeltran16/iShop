import { Pipe, PipeTransform } from '@angular/core';


@Pipe({
    name: 'fill',
    pure: false
})
//
export class FillPipe implements PipeTransform {
    transform(items: any[], fillStart: number, fillEnd: number): any[] {
        if (!items || !fillStart || !fillEnd) {
            return items;
        }
        return items.filter((p, i: any, ps: any) => i < fillEnd && i > fillStart);
    }
}  