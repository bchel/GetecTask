import React, { Component } from 'react';

export class WordyFormatter extends Component {
    displayName = WordyFormatter.name

    constructor(props) {
        super(props);
        this.state = { number: '', text: '' };
        //this.getWordy = this.getWordy.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    getWordy() {
        //this.setState({
        //    number: this.state.number + 1
        //});
    }

    handleChange(e) {
        this.setState({ number: e.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();
        if (!this.state.number.length) {
            return;
        }

        fetch(`api/SampleData/WordyFormatter?input=${encodeURIComponent(this.state.number)}`)
            .then(response => response.json())
            .then(data => {
                this.setState({ text: data.wordyOutput });
            });

        //this.setState(state => ({
        //    number: state.number,
        //    text: 'abc'
        //}));
    }

    render() {
        return (
            <div>
                <h1>Wordy Formatter</h1>

                <p>Word presentation: <strong>{this.state.text}</strong></p>

                <form onSubmit={this.handleSubmit}>
                    <div class="form-group">
                        <label htmlFor="number-input">
                            Enter a number:
                        </label>
                        <input
                            id="number-input"
                            class="form-control"
                            onChange={this.handleChange}
                            value={this.state.number}
                        />
                    </div>
                    
                    <button type="submit" class="btn btn-primary">
                        Convert
                    </button>
                </form>
            </div>
        );
    }
}