import React, { Component } from 'react';
import {Route, Routes} from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './pages/Home';
import { FetchData } from './pages/FetchData';
import { Counter } from './pages/Counter';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
          <Routes>
            <Route exact path='/' element={<Home />} />
            <Route path='/counter' element={<Counter />} />
            <Route path='/fetch-data' element={<FetchData />} />
          </Routes>
      </Layout>
    );
  }
}
