function init()
{
    google.charts.load('current', { 'packages': ['gantt'] });
    google.charts.setOnLoadCallback(drawChart);
}



function daysToMilliseconds(days) {
    return days * 24 * 60 * 60 * 1000;
}

function drawChart() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Task ID');
    data.addColumn('string', 'Task Name');
    data.addColumn('string', 'Resource');
    data.addColumn('date', 'Start Date');
    data.addColumn('date', 'End Date');
    data.addColumn('number', 'Duration');
    data.addColumn('number', 'Percent Complete');
    data.addColumn('string', 'Dependencies');

    data.addRows([
        ['T1', 'Spring 1', 'spring',
            new Date(2014, 2, 22), new Date(2014, 3, 20), null, 100, null],
        ['T1', 'Spring 2', 'summer',
            new Date(2014, 7, 21), new Date(2014, 8, 20), null, 100, null],
        ['T2', 'Autumn 2014', 'autumn',
            new Date(2014, 8, 21), new Date(2014, 11, 20), null, 100, null],
        
    ]);

    var options = {
        height: 275
    };

    var chart = new google.visualization.Gantt(document.getElementById('chart'));

    chart.draw(data, options);
}