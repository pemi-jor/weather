import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  chart: any;
  weathers: Weather[] = [];
  locations: Location[] = [];
  
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  locationId: number | undefined;
  selectedLocationName: string = '';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Location[]>(baseUrl + 'locations').subscribe(
      {
        next: (r) => this.locations = r,
        error: (e) => console.error(e)
      }
    );
  }
  
  getWeather(http: HttpClient, @Inject('BASE_URL') baseUrl: string): void {
    //checks for values
    http.get<Weather[]>(baseUrl + 'weathers?' + this.locationId + '&' + this.range.value.start + '&' + this.range.value.end).subscribe({
      next: (n) => this.weathers = n,
      error: (e) => console.error(e)
    });
    
  }

  chartOptions = {
    animationEnabled: true,
    theme: "dark1",
    title: {
      text: this.locations.find(l => l.id === this.locationId)?.name || "Valitse paikkakunta"
    },
    axisY: {
      title: "Tuulen nopeus (ms)",
      includeZero: true,
      labelFormatter: (e: any) => {
        return e.value + ' ms';
      }
    },
    axisY2: {
      title: "Lämpötila (°C)",
      includeZero: true,
      labelFormatter: (e: any) => {
        return e.value + ' °C';
      }
    },
    toolTip: {
      shared: true
    },
    legend: {
      cursor: "pointer",
      itemclick: function (e: any) {
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
          e.dataSeries.visible = false;
        } else {
          e.dataSeries.visible = true;
        }
        e.chart.render();
      }
    },
    data: [{
      type: "column",
      showInLegend: true,
      name: "Revenue",
      axisYType: "secondary",
      yValueFormatString: "$#,###",
      dataPoints: [
        this.weathers.map(w => { w.date, w.windSpeed })
      ]
    }, {
      type: "spline",
      showInLegend: true,
      name: "No of Orders",
      dataPoints: [
        this.weathers.map(w => { w.date, w.temperatureC })
      ]
    }]
  }


}


interface Location {
  id: number;
  name: string;
}

interface Weather {
  date: string;
  temperatureC: number;
  windSpeed: number;
  locationId: number;
}
