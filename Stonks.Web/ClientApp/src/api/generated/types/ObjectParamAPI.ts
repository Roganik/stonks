import { ResponseContext, RequestContext, HttpFile } from '../http/http';
import * as models from '../models/all';
import { Configuration} from '../configuration'

import { WeatherForecast } from '../models/WeatherForecast';

import { ObservableWeatherForecastApi } from "./ObservableAPI";
import { WeatherForecastApiRequestFactory, WeatherForecastApiResponseProcessor} from "../apis/WeatherForecastApi";

export interface WeatherForecastApiWeatherForecastGetRequest {
}

export class ObjectWeatherForecastApi {
    private api: ObservableWeatherForecastApi

    public constructor(configuration: Configuration, requestFactory?: WeatherForecastApiRequestFactory, responseProcessor?: WeatherForecastApiResponseProcessor) {
        this.api = new ObservableWeatherForecastApi(configuration, requestFactory, responseProcessor);
    }

    /**
     * @param param the request object
     */
    public weatherForecastGet(param: WeatherForecastApiWeatherForecastGetRequest, options?: Configuration): Promise<Array<WeatherForecast>> {
        return this.api.weatherForecastGet( options).toPromise();
    }

}
