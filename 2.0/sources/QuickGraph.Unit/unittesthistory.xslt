<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:include href ="common.xslt"/>
    <xsl:key name="assemblies" match="TestBatch" use="@EntryAssemblyName"/>
	<xsl:key name="testcases" match="TestCase" use="@Name"/>
	<xsl:key name="fixtures" match="Fixture" use="@Name"/>
	<xsl:key name="machines" match="Machine" use="concat(@MachineName,concat(@FrameworkVersion,@OperatingSystem))"/>
<xsl:template match="/MergedBatch">
	<html>
		<xsl:comment> saved from url=(0027)http://blog.dotnetwiki.org/ </xsl:comment>
		<head>
                <title>Test Summary</title>
                <xsl:call-template name="meta-html" />
            </head>
            <body>
                <h1>
                    <img src="unit.png" border="0" /><xsl:text> </xsl:text>
                    Test Summary
                </h1>
				<xsl:call-template name="assemblies-summary" />
				<xsl:call-template name="assembly-details" />
				<xsl:call-template name="fixture-history" />
				<xsl:call-template name="machine-summaries" />
                <xsl:call-template name="unit-copyright" />
            </body>
        </html>
    </xsl:template>

	<xsl:template name="assemblies-summary">
    <table border="0" cellpadding="1" cellspacing="1" width="100%">
    <tr class="heading">
    	<td>Name</td>
    	<td>Success (average)</td>
    	<td><small><xsl:call-template name="counter-figures-legend" /></small></td>
    	<td>Duration</td>
    </tr>
	<xsl:for-each select="TestBatches/TestBatch[count(.|key('assemblies',@EntryAssemblyName)[1]) = 1]">
		<xsl:sort order="descending" data-type="text" select="@EndTime" />
		<xsl:call-template name="assembly-summary" />		
	</xsl:for-each>
	</table>
	</xsl:template>
	
	<xsl:template name="assembly-summary">
	<xsl:variable name="assemblyname" select="@EntryAssemblyName" />
	<xsl:variable name="testbatches" select="ancestor::TestBatches/TestBatch[@EntryAssemblyName = $assemblyname]" />
	<tr>
		<xsl:call-template name="testbatch-class"/>
		<td>
			<a>
				<xsl:attribute name="href">#sec<xsl:value-of select="generate-id()"/></xsl:attribute>
				<xsl:value-of select="@EntryAssemblyName" />
			</a>
		</td>
		<td>
			<xsl:call-template name="counter-percent"/>
			(
			<xsl:call-template name="counter-average-percent">
				<xsl:with-param name="tests" select="$testbatches" />
			</xsl:call-template>
			)
		</td>
		<td>
			<xsl:call-template name="counter-figures" />
		</td>
		<td>
			<xsl:call-template name="duration-split" />
		</td>
	</tr>
	</xsl:template>
	
	<xsl:template name="assembly-details">
	<xsl:for-each select="TestBatches/TestBatch[count(.|key('assemblies',@EntryAssemblyName)[1]) = 1]">
		<xsl:sort data-type="text" select="@EntryAssemblyName" />
		<xsl:call-template name="assembly-detail" />
	</xsl:for-each>		
	</xsl:template>

	<xsl:template name="assembly-detail">
	<xsl:variable name="assemblyname" select="@EntryAssemblyName" />
	<xsl:variable name="testbatches" select="ancestor::TestBatches/TestBatch[@EntryAssemblyName = $assemblyname]" />
	<h3>
		<xsl:attribute name="id">sec<xsl:value-of select="generate-id()"/></xsl:attribute>
		<xsl:value-of select="@EntryAssemblyName" />
	</h3>
	<h4>
	<xsl:call-template name="toggle">
		<xsl:with-param name="id">batchhistory</xsl:with-param>
	</xsl:call-template>
	Batch History</h4>
	<div id="batchhistory" class="toggle">
    <table border="0" cellpadding="1" cellspacing="1" width="100%">
    <tr class="heading">
    	<td>
    		Time <small>(yMMdd_hhmmss)</small>
    	</td>
    	<td>
    		Success
    	</td>
    	<td>
    		<small><xsl:call-template name="counter-figures-legend" /></small>
    	</td>
    	<td>
    		Duration
    	</td>
    </tr>
	<xsl:for-each select="$testbatches">
		<xsl:sort order="descending" data-type="text" select="@EndTime" />
		<tr>
			<xsl:call-template name="testbatch-class"/>
			<td>
				<a target="_blank">
					<xsl:attribute name="href"><xsl:value-of select="@Path" /></xsl:attribute>
					<xsl:value-of select="@EndTime" />
				</a>
			</td>
			<td>
				<xsl:call-template name="counter-percent" />
			</td>
			<td>
				<xsl:call-template name="counter-figures" />
			</td>
			<td>
				<xsl:call-template name="duration-split" />
			</td>
		</tr>
	</xsl:for-each>	
	</table>
	</div>
	</xsl:template>
	
	<xsl:template name="counter-average-percent">
	<xsl:param name="tests" />
	<xsl:variable name="total" select="sum($tests/Counter/@TotalCount) - sum($tests/Counter/@IgnoreCount)"/>
	<xsl:variable name="success" select="sum($tests/Counter/@SuccessCount)"/>
	<xsl:choose>
		<xsl:when test="$total = 0">100.00</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="format-number($success div $total * 100.0,'0.00')"/>
		</xsl:otherwise>
	</xsl:choose>%
	</xsl:template>

	<xsl:template name="average-class">
	<xsl:param name="tests" />
	<xsl:variable name="total" select="sum($tests/Counter/@TotalCount) - sum($tests/Counter/@IgnoreCount)"/>
	<xsl:variable name="success" select="sum($tests/Counter/@SuccessCount)"/>
	<xsl:variable name="average" select="$success div $total * 100.0" />
	<xsl:choose>
		<xsl:when test="$total = 0 or $average = 100">
			<xsl:call-template name="tr-success-class" />
		</xsl:when>
		<xsl:when test="$ average > 0">
			<xsl:call-template name="tr-failure-class" />
		</xsl:when>
	</xsl:choose>	
	</xsl:template>

	<xsl:template name="fixture-history">
	<h4>
	<xsl:call-template name="closed-toggle">
		<xsl:with-param name="id">fixtureHistory</xsl:with-param>
	</xsl:call-template>
		Fixture history</h4>
	<div id="fixtureHistory" class="closedToggle">
    <table border="0" cellpadding="1" cellspacing="1" width="100%">
    <tr class="heading">
    	<td>
    		Name
    	</td>
    	<td>Run count</td>
    	<td>
    		Success average
    	</td>
    </tr>
	<xsl:for-each select="TestBatches/TestBatch/TestAssemblies/TestAssembly/Fixtures/Fixture[count(.|key('fixtures',@Name)[1]) = 1]">
		<xsl:sort order="descending" select="@EndTime" />
		<xsl:call-template name="fixture-history-row" />
	</xsl:for-each>
	</table>
	</div>
	</xsl:template>
	
	<xsl:template name="fixture-history-row">
	<xsl:variable name="fixturename" select="@Name" />
	<xsl:variable name="fixtures" select="//Fixture[@Name = $fixturename]" />
	<tr>
		<xsl:call-template name="average-class">
			<xsl:with-param name="tests" select="$fixtures" />
		</xsl:call-template>
		<td>
			<xsl:value-of select="$fixturename" />
		</td>
		<td>
			<xsl:value-of select="count($fixtures)" />
		</td>
		<td>
			<xsl:call-template name="counter-average-percent">
				<xsl:with-param name="tests" select="$fixtures" />
			</xsl:call-template>
		</td>
	</tr>
	</xsl:template>

	<xsl:template name="machine-summaries">
	<h4>
	<xsl:call-template name="toggle">
		<xsl:with-param name="id">machines</xsl:with-param>
	</xsl:call-template>
	Machines
	</h4>
	<div class="toggle" id="machines">
    <table border="0" cellpadding="1" cellspacing="1" width="100%">
    <tr class="heading">
    	<td>
    		Name
    	</td>
    	<td>.NET</td>
    	<td>
    		OS
    	</td>
    </tr>
		<xsl:for-each select="TestBatches/TestBatch/Machine[count(.|key('machines',concat(@MachineName,concat(@FrameworkVersion,@OperatingSystem)))[1]) = 1]">
		<xsl:call-template name="machine-row" />
		</xsl:for-each>
	</table>
	</div>
	</xsl:template>	

	<xsl:template name="machine-row">
		<xsl:variable name="machineid" select="generate-id()" />
		<tr>
			<xsl:call-template name="tr-class"/>
			<td>
				<xsl:call-template name="closed-toggle">
					<xsl:with-param name="id" select="$machineid" />
				</xsl:call-template>				
				<xsl:value-of select="@MachineName" />
				<div class="closedToggle">
					<xsl:attribute name="id"><xsl:value-of select="$machineid" /></xsl:attribute>
					<xsl:call-template name="machine-description" />
				</div>
			</td>
			<td>
				<xsl:value-of select="@FrameworkVersion" />
			</td>
			<td>
				<xsl:value-of select="@OperatingSystem" />
			</td>
		</tr>
	</xsl:template>	

</xsl:stylesheet>