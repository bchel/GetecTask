import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { WordyFormatter } from './components/WordyFormatter';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
            <Route exact path='/' component={WordyFormatter} />
            <Route path='/wordy' component={WordyFormatter} />
      </Layout>
    );
  }
}
