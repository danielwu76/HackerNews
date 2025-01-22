import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class WeatherForecastService {
  private apiUrl = 'https://localhost:7036/api/WeatherForecast';

  constructor(private http: HttpClient) { }

  getWeatherForecast(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
