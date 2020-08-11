<template>
    <form novalidate @submit.stop.prevent="showSnackbar = true">
        {{balance}}
        <div class="list-container">
            <div class="transaction-form">
                <md-field class="field">
                    <label for="movie">Type</label>
                    <md-select v-model="type" id="type">
                        <md-option value="debit">Debit</md-option>
                        <md-option value="credit">Credit</md-option>
                    </md-select>
                </md-field>
                <md-field class="field">
                    <label>Amount</label>
                    <md-input v-model="amount" type="number" min="0" max="100"></md-input>
                </md-field>
                <md-button class="md-raised md-primary" v-on:click="post">Post</md-button>
            </div>
            <md-toolbar v-bind:class="transaction.type === 'debit' ? 'md-primary' : 'md-accent'" class="row" v-for="transaction in transactions" :key="transaction.id">{{ `${transaction.type} ${transaction.amount} ${new Date(transaction.effectiveTime)}` }}</md-toolbar>
        </div>
        <md-snackbar :md-position="position" :md-duration="600" :md-active.sync="showSnackbar" md-persistent>
            <span>{{error}}</span>
        </md-snackbar>
    </form>
</template>

<script>
import axios from 'axios';
export default {
    name: 'transaction-list',
    data() {
        return {
            transactions: null,
            amount: 2,
            type: "debit",
            error: null,
            showSnackbar: false,
            position: "center",
            balance: 0,
        }
    },
    methods: {
        post: function() {
            axios.post('http://localhost:5000/transaction', {
                amount: Number(this.amount),
                type: this.type,
            }).then(res => {
                if (res.status === 200) {
                        this.init();
                }
            }).catch(error => {
                this.error = error.response.data.detail;
                this.showSnackbar = true;
            });           
        },
        init: function() {
            axios.get('http://localhost:5000/transaction')
                .then(res => {
                    if (res.status === 200) {
                        this.transactions = res.data;
                    }
                 });
             axios.get('http://localhost:5000/balance').then(res => {
                        
                    if (res.status === 200) {
                            this.balance = res.data.balance;
                        }
                    });
        }
    },
    mounted() {
        this.init();
    }
}
</script>

<style>
.list-container {
    display: flex;
    flex-direction: column;
    margin: 0 10%;
}
.transaction-form {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
}
.field {
    width: 20% !important;
}
.row {
    margin-top: 10px;
}
</style>