import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HackerNewsService {
  private apiBaseUrl = 'https://localhost:7036/api/HackerNews'; 
  constructor(private http: HttpClient) { }

  // Fetch top stories
  getTopStories(): Observable<number[]> {
    return this.http.get<number[]>(`${this.apiBaseUrl}/`);
  }

  // Fetch details of a specific story
  getStory(itemId: number): Observable<any> {
    return this.http.get<any>(`${this.apiBaseUrl}/item/${itemId}`);
  }
}
