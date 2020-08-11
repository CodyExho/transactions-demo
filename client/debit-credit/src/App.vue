<template>
  <div id="app">
    <transaction-list/>
  </div>
</template>

<script>
import transactionList from './components/transactions-list.vue'

export default {
  name: 'App',
  components: {
    transactionList,
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
