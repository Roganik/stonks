import { ResponseContext, RequestContext, HttpFile } from '../http/http';
import * as models from '../models/all';
import { Configuration} from '../configuration'
import { Observable, of, from } from '../rxjsStub';
import {mergeMap, map} from  '../rxjsStub';
import { WeatherForecast } from '../models/WeatherForecast';

import { WeatherForecastApiRequestFactory, WeatherForecastApiResponseProcessor} from "../apis/WeatherForecastApi";
export class ObservableWeatherForecastApi {
    private requestFactory: WeatherForecastApiRequestFactory;
    private responseProcessor: WeatherForecastApiResponseProcessor;
    private configuration: Configuration;

    public constructor(
        configuration: Configuration,
        requestFactory?: WeatherForecastApiRequestFactory,
        responseProcessor?: WeatherForecastApiResponseProcessor
    ) {
        this.configuration = configuration;
        this.requestFactory = requestFactory || new WeatherForecastApiRequestFactory(configuration);
        this.responseProcessor = responseProcessor || new WeatherForecastApiResponseProcessor();
    }

    /**
     */
    public weatherForecastGet(_options?: Configuration): Observable<Array<WeatherForecast>> {
        const requestContextPromise = this.requestFactory.weatherForecastGet(_options);

        // build promise chain
        let middlewarePreObservable = from<RequestContext>(requestContextPromise);
        for (let middleware of this.configuration.middleware) {
            middlewarePreObservable = middlewarePreObservable.pipe(mergeMap((ctx: RequestContext) => middleware.pre(ctx)));
        }

        return middlewarePreObservable.pipe(mergeMap((ctx: RequestContext) => this.configuration.httpApi.send(ctx))).
            pipe(mergeMap((response: ResponseContext) => {
                let middlewarePostObservable = of(response);
                for (let middleware of this.configuration.middleware) {
                    middlewarePostObservable = middlewarePostObservable.pipe(mergeMap((rsp: ResponseContext) => middleware.post(rsp)));
                }
                return middlewarePostObservable.pipe(map((rsp: ResponseContext) => this.responseProcessor.weatherForecastGet(rsp)));
            }));
    }

}
