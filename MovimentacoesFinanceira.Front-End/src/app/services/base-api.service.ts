import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IApiResponse } from '../Interface/IApiResponse';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class BaseApiService {
  private apiUrl = 'https://localhost:7201/MovimentacoesFinanceira/'; // URL da API

  constructor(private httpClient: HttpClient) { }

  protected getAll<TData>(path: string): Observable<IApiResponse<TData[]>> {
    return this.httpClient.get<IApiResponse<TData[]>>(this.apiUrl + path);
  }

  protected uploadFile<TData>(file: File, path: string): Observable<IApiResponse<TData>> {
    const formData = new FormData();
    formData.append('file', file);

    return this.httpClient.post<IApiResponse<TData>>(this.apiUrl + path, formData);
  }

}
