import '../lib/chart.min.js'

var chart;
var samples = 100;
var speed = 250;
var values = [];
var labels = [];

export function init() {
    values.length = samples;
    labels.length = samples;
    // populate arrays
    values.fill(0);
    labels.fill(0);

    chart = new Chart(document.getElementById("chart"), getConfig());
}

function getConfig() {
    return {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                {
                    data: values,
                    backgroundColor: 'rgba(255, 99, 132, 0.1)',
                    borderColor: 'rgba(112, 146, 190, 0.9)',
                    borderWidth: 2,
                    lineTension: 0.25,
                    pointRadius: 0
                }
            ]
        },
        options: {
            responsive: false,
            animation: {
                duration: speed * 1.5,
                easing: 'linear'
            },
            legend: false,
            scales: {
                xAxes: [
                    {
                        display: false
                    }
                ],
                yAxes: [
                    {
                        ticks: { max: 1, min: -1 }
                    }
                ]
            }
        }
    };
}
// called from blazor component
export function updateChart(pulse) {
    values.push(pulse.value);
    values.shift();
    chart.update();
}
