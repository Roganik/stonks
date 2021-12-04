import { Component } from 'react';
import {WeatherForecastApi, WeatherForecast, createConfiguration, Configuration} from "../api/index";

export class FetchData extends Component<any, {loading: boolean, forecasts: WeatherForecast[]}> {
  static displayName = FetchData.name;
  private api: WeatherForecastApi;

  constructor(props : any) {
    super(props);
    this.state = { forecasts: [], loading: true };
    const config : Configuration = createConfiguration();
    this.api = new WeatherForecastApi(config);
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts : any[]) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const data = await this.api.weatherForecastGet();
    this.setState({ forecasts: data, loading: false });
  }
}
