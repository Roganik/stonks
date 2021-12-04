import { ResponseContext, RequestContext, HttpFile } from '../http/http';
import * as models from '../models/all';
import { Configuration} from '../configuration'

import { WeatherForecast } from '../models/WeatherForecast';
import { ObservableWeatherForecastApi } from './ObservableAPI';

import { WeatherForecastApiRequestFactory, WeatherForecastApiResponseProcessor} from "../apis/WeatherForecastApi";
export class PromiseWeatherForecastApi {
    private api: ObservableWeatherForecastApi

    public constructor(
        configuration: Configuration,
        requestFactory?: WeatherForecastApiRequestFactory,
        responseProcessor?: WeatherForecastApiResponseProcessor
    ) {
        this.api = new ObservableWeatherForecastApi(configuration, requestFactory, responseProcessor);
    }

    /**
     */
    public weatherForecastGet(_options?: Configuration): Promise<Array<WeatherForecast>> {
        const result = this.api.weatherForecastGet(_options);
        return result.toPromise();
    }


}



