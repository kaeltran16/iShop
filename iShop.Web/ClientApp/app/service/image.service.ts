import { Injectable, Inject } from '@angular/core';
import { Http, RequestOptions, Headers} from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';



@Injectable()
export class ImageService {
    Url: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.Url = baseUrl;
    }

  
 
     // get Categories
    getImages() {
        return this.http.get(this.Url + 'api/Images')
            .map(res => res.json());

    }
  


    // Create image
    createImages(event: any,productId:string,token:string) {

        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();
            formData.append('file', file, file.name);
            let headers = new Headers();
            /** No need to include Content-Type in Angular 4 */
            headers.append('Content-Type', 'multipart/form-data');
            headers.append('Accept', 'application/json');
            headers.append('withCredentials', 'true');
            headers.append('Authorization', 'Bearer ' + token);
            let options = new RequestOptions(({ headers: headers }));
            return this.http.post(this.Url + 'api/product/' + productId + '/Images', formData,
                    ({
                        headers: {
                            //USE credentials mode
                            Accept: 'application/json',
                            'Content-Type':'multipart/form-data',
                            withCredentials: true,
                            'Authorization': 'Bearer ' + token
                        }
                    }) as any)
                .map(res => res.json()).catch(error => Observable.throw(error))
                .subscribe(
                    data => console.log('success'),
                    error => console.log(error)
                );

        }

    }
}