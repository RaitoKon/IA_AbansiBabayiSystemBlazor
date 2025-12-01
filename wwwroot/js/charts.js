window.renderStockDonut = (elementId, data, labels) => {
    var options = {
        chart: {
            type: 'donut',
            height: 250
        },
        series: data,
        labels: labels,
        colors: ['#35d97aff', 'rgba(255,152,0,1)', 'rgba(244,67,54,1)', 'rgba(66,66,66,1)'],
        legend: {
            position: 'bottom'
        },
        plotOptions: {
            pie: {
                donut: {
                    size: '65%',
                    labels: {
                        show: true,
                        total: {
                            show: true,
                            label: 'Total',
                            fontSize: '22px'
                        }
                    }
                }
            }
        }
    };

    var chart = new ApexCharts(document.querySelector('#' + elementId), options);
    chart.render();
};

window.renderAreaChart = (elementId, seriesData, categories) => {
    var series = [
        { name: 'Sales', data: seriesData[0] },
        { name: 'Returns', data: seriesData[1] },
        { name: 'Damages', data: seriesData[2] }
    ];

    var options = {
        series: series,
        chart: {
            type: 'area',
            width: '100%',
            height: 200,
            toolbar: { show: false }
        },
        colors: ['#ffffff', 'rgba(255,152,0,1)', 'rgba(244,67,54,1)'],
        dataLabels: { enabled: false },
        stroke: {
            curve: 'smooth',
            width: 1
        },
        xaxis: {
            categories: categories,
            labels: { style: { colors: '#ffffff', fontSize: '12px' } }
        },
        yaxis: {
            labels: {
                style: { colors: '#ffffff', fontSize: '12px' },
                formatter: function (val) {
                    return '₱ ' + val.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                }
            }
        },
        fill: {
            type: 'gradient',
            gradient: {
                shade: 'light',
                type: 'vertical',
                shadeIntensity: 0,
                gradientToColors: ['#ffffff'],
                opacityFrom: 1,
                opacityTo: 0,
                stops: [0, 100]
            }
        },
        legend: {
            show: true,
            position: 'bottom',
            horizontalAlign: 'center',
            labels: { colors: '#ffffff' },
            showForSingleSeries: true,
            itemMargin: { horizontal: 10 }
        },
        tooltip: {
            enabled: true,
            theme: 'dark', // makes the tooltip background dark
            style: {
                fontSize: '12px',
                fontFamily: undefined
            },
            y: {
                formatter: function (val) {
                    return '₱ ' + val.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                }
            }
        }
    };

    new ApexCharts(document.querySelector('#' + elementId), options).render();
};