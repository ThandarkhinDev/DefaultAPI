import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { toODataString } from '@progress/kendo-data-query';

const httpOptionsJson = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json',
    'Accept': 'application/json' 
    })
};

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  public loading: boolean;
  constructor(private http: HttpClient) { }
  private formatErrors(error: any) {
    return  throwError(error.error);
  }

  log(arg0: any): any {
    throw new Error('Method not implemented.');
  }

  uploadFile(path: string, body) {
    return this.http.post(
      `${environment.api_url}${path}`, (body)
    );
  }

  // grid post.
  fetchgrid_postJson(path: string, state: any): Observable<GridDataResult> {
    this.loading = true;
    return this.http.post(`${environment.api_url}${path}`, state )
        .pipe(
            map(response => (<GridDataResult>{
                data: response['data'],
                total: parseInt(response['dataFoundRowsCount'], 10)
            })),
            tap(() => this.loading = false)
        );
  }

  postJson(path: string, body: Object = {}): Observable<any> {
    return this.http.post(
      `${environment.api_url}${path}`,
      JSON.stringify(body), httpOptionsJson
    ).pipe(
      map(response => response['data']),
      catchError(this.formatErrors)
    );
  }

  get(path: string): Observable<any> {
    return this.http.get(`${environment.api_url}${path}`)
      .pipe(catchError(this.formatErrors));
  }

  delete(path): Observable<any> {
    return this.http.delete(
      `${environment.api_url}${path}`
    ).pipe(
      map(response => response['data']),
      catchError(this.formatErrors));
  }
}
