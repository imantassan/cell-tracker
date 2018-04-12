import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { CellLog, Home } from './components';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/celllog/:dateFrom?/:dateTo?' component={ CellLog } />
</Layout>;
