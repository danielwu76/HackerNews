import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HackerNewsService {
  private apiBaseUrl = 'https://localhost:7036/api/HackerNews'; 
  constructor(private http: HttpClient) { }

  getTopItems(): Observable<number[]> {
    return this.http.get<number[]>(`${this.apiBaseUrl}/`);
  }

  getItem(itemId: number): Observable<any> {
    return this.http.get<any>(`${this.apiBaseUrl}/item/${itemId}`);
  }
}
