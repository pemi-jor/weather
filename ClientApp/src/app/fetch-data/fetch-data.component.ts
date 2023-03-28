import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder, FormControl, FormArray } from '@angular/forms';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  FeedBack!: FormGroup;
  public forecasts: Weather[] = [];
  weather: Weather = {
    date: '',
    temperatureC: 0,
    windSpeed: 0,
    locationId: 0
  };
  public locations: Location[] = [];
  public location: string = '';

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    http.get<Weather[]>(baseUrl + "weatherforecast").subscribe(
      {
        next: (r) => console.log(r),
        error: (e) => console.error(e)
      }
    );
    http.get<Location[]>(baseUrl + 'location').subscribe(
      {
        next: (r) => this.locations = r,
        error: (e) => console.error(e)
      }
    );
  }

  setForms(arr: Array<Weather>): void {
    arr.forEach((row) => this.formArr.controls.push(Object.create(row)))
    
  }

  get formArr() {
    return this.FeedBack.get('Rows') as FormArray;
  }

  addRow(): void {
    let newWeather: Weather = {
      date: '',
      temperatureC: 0,
      windSpeed: 0,
      locationId: 0
    };
    this.formArr.push(newWeather);
  }

  saveRow(index: number): void {
    this.http.post<Weather>(this.baseUrl + 'WeatherForecast', this.formArr.at(index)).subscribe({
      next: (n) => console.info(n),
      error: (e) => console.error(e)
    })
  }

  deleteRow(index: number): void{
    let id: number = this.formArr.at(index).value.id;
    this.http.delete(this.baseUrl + 'WeatherForecast/' + id).subscribe({
      next: (n) => this.formArr.removeAt(index),
      error: (e) => console.error(e)
    })
  }

  saveLocation(): void {
    this.http.post(this.baseUrl + 'location?location=' + this.location, {}).subscribe({
      next: (n) => console.info(n),
      error: (e) => console.error(e)
    })
  }
  
}


interface Location {
  id: number;
  name: string;
}

interface Weather {
  id?: number,
  date: string;
  temperatureC: number;
  windSpeed: number;
  locationId: number;
}
